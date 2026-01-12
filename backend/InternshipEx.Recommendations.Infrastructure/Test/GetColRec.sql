-- 1. Internship IDs liked/applied by current user
WITH UserLikes AS (
    SELECT DISTINCT InternshipId
    FROM [DyplomkaDb].[recommendations].[Interactions]
    WHERE UserId = @UserId
      AND Type IN ('Liked', 'Applied')
),

-- 2. Similar users = users who liked the same internships
SimilarUsers AS (
    SELECT DISTINCT UserId
    FROM [DyplomkaDb].[recommendations].[Interactions]
    WHERE InternshipId IN (SELECT InternshipId FROM UserLikes)
      AND UserId <> @UserId
      AND Type IN ('Liked', 'Applied')
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
      AND i.Type IN ('Liked', 'Applied')
    GROUP BY i.InternshipId
)

SELECT 
    intern.*,
    CAST((r.DistinctUsers * 100.0) / suc.Cnt AS FLOAT) AS Score
FROM Rec r
JOIN [DyplomkaDb].[interships].[Internships] intern ON intern.Id = r.InternshipId
CROSS JOIN SimilarUsersCount suc
ORDER BY Score DESC;
