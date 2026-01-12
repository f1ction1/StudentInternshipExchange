using Dapper;
using InternshipEx.Recommendations.Domain.Interactions;
using InternshipEx.Recommendations.Persistence;
using Modules.Common.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using InternshipEx.Recommendations.Infrastructure.Test;

namespace InternshipEx.Recommendations.Infrastructure.Controllers
{
    [ApiController]
    [Authorize(Roles = "student")]
    [Route("api/interactions/")]
    public class InteractionController(
        RecDbContext dbContext,
        CurrentUserService currentUserService) : ControllerBase
    {
        [HttpPost("{internshipId}")]
        public async Task<IActionResult> AddInteraction([FromRoute]Guid internshipId, [FromBody]string type)
        {
            var studentId = currentUserService.UserId;
            var existingInteraction = await dbContext.Interactions
                .FirstOrDefaultAsync(i => i.UserId == studentId && i.InternshipId == internshipId && i.Type.ToString() == type);
            if (existingInteraction is not null)
            {
                // Interaction already exists, no need to add a duplicate
                return Ok();
            }
            var interaction = Interaction.Create(
                userId: studentId,
                internshipId: internshipId,
                type: Enum.Parse<InteractionType>(type),
                timeStamp: DateTime.UtcNow);
            await dbContext.Interactions.AddAsync(interaction);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("colab-rec/{takeNumber}")] // (Collaborative filtering) = on other users
        //ми не повертаємо вакансії, які юзер вже бачив / лайкав;
        //шукаємо інших людей з перетином вподобань;
        //повертаємо вакансії яких користувач ще не бачив.
        public async Task<IActionResult> GetCollaborativeFiltering(int takeNumber = 100)
        {
            var userId = currentUserService.UserId;
            // вакансії юзера
            var userInternships = await dbContext.Interactions
                .Where(x => x.UserId == userId &&
                            (x.Type == InteractionType.Liked ||
                             x.Type == InteractionType.Applied ||
                                x.Type == InteractionType.Viewed))
                .Select(x => x.InternshipId)
                .Distinct()
                .ToListAsync();

            if (!userInternships.Any())
                return NoContent();

            // 1. Знаходимо схожих користувачів
            var similarUsers = await dbContext.Interactions
                .Where(i => userInternships.Contains(i.InternshipId)
                            && i.UserId != userId
                            && (i.Type == InteractionType.Liked ||
                                i.Type == InteractionType.Applied ||
                                i.Type == InteractionType.Viewed))
                .Select(i => i.UserId)
                .Distinct()
                .ToListAsync();

            if (!similarUsers.Any())
                return NoContent();

            // 2. Беремо інші вакансії схожих
            var recommendations = await dbContext.Interactions
                .Where(i => similarUsers.Contains(i.UserId)
                            && !userInternships.Contains(i.InternshipId)
                            && (i.Type == InteractionType.Liked ||
                                i.Type == InteractionType.Applied ||
                                i.Type == InteractionType.Viewed))
                .GroupBy(i => i.InternshipId)
                .Select(g => new
                {
                    InternshipId = g.Key,
                    Score = Math.Round((double)g
                        .Select(x => x.UserId)
                        .Distinct()
                        .Count() / similarUsers.Count * 100, 2)
                })
                .OrderByDescending(x => x.Score)
                .Take(takeNumber)
                .ToListAsync();

            return Ok(recommendations);
        }

        [HttpGet("test/{take}")]
        public async Task<IActionResult> GetTest(int take = 100)
        {
            var userId = currentUserService.UserId;
            using var connection = new SqlConnection(dbContext.Database.GetConnectionString());
            string sql = """
                -- 1. Internship IDs liked/applied by current user
                WITH UserLikes AS (
                    SELECT DISTINCT InternshipId
                    FROM [DyplomkaDb].[recommendations].[Interactions]
                    WHERE UserId = @UserId
                      AND Type IN (2, 4)
                ),

                -- 2. Similar users = users who liked the same internships
                SimilarUsers AS (
                    SELECT DISTINCT UserId
                    FROM [DyplomkaDb].[recommendations].[Interactions]
                    WHERE InternshipId IN (SELECT InternshipId FROM UserLikes)
                      AND UserId <> @UserId
                      AND Type IN (2, 4)
                ),

                -- 3. Count similar users (for score calculation)
                SimilarUsersCount AS (
                    SELECT COUNT(*) AS Cnt FROM SimilarUsers
                ),

                -- 4. Recommendations (internships liked by similar users)
                Rec AS (
                    SELECT 
                        i.InternshipId,
                        COUNT(DISTINCT i.UserId) AS DistinctUsers
                    FROM [DyplomkaDb].[recommendations].[Interactions] i
                    WHERE i.UserId IN (SELECT UserId FROM SimilarUsers)
                      AND i.InternshipId NOT IN (SELECT InternshipId FROM UserLikes)
                      AND i.Type IN (2, 4)
                    GROUP BY i.InternshipId
                )

                SELECT TOP(@Take)
                    intern.Id,
                    intern.Title,
                    intern.EmployerName,
                    intern.IsRemote,
                    cities.Name as CityName,
                    counties.Name as CountryName,
                    CAST((r.DistinctUsers * 100.0) / suc.Cnt AS FLOAT) AS Score
                FROM Rec r
                JOIN [DyplomkaDb].[interships].[Internships] intern ON intern.Id = r.InternshipId
                JOIN [interships].[Cities] cities ON intern.CityId = cities.Id
                JOIN [interships].Countries counties ON cities.CountryId = counties.Id
                CROSS JOIN SimilarUsersCount suc
                ORDER BY Score DESC;
                
                """;

            var result = await connection.QueryAsync<InternshipRecommendationDto>(sql, new { UserId = userId, Take = take });
            return Ok(result);
        }
    }
}
