import { useQuery } from "@tanstack/react-query";
import { getInternshipsList } from "../api/internshipApi";

export function useInternships(filters) {
  return useQuery({
    queryKey: ["internships", filters],
    queryFn: async () => {
      const params = new URLSearchParams(filters);
      const res = await getInternshipsList(params)
      if (res.status != 200) throw new Error("Failed to fetch internships");
      return res.data
    },
    keepPreviousData: true, // dont clear previous results
  });
}