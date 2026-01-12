import { useQuery } from "@tanstack/react-query";
import { getInternshipDictionaries } from "../api/internshipApi";

export function useDictionaries() {
  return useQuery({
    queryKey: ["dictionaries"],
    queryFn: async () => {
      const res = await getInternshipDictionaries();
      if (res.status != 200) throw new Error("Failed to fetch dictionaries");
      return res.data;
    },
    staleTime: 1000 * 60 * 60, // 1 hour not update
    gcTime: 1000 * 60 * 60 * 12 // half of the day in cache
  });
}