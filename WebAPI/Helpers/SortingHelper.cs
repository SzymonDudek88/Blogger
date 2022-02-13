using System.Collections.Generic;

namespace WebAPI.Helpers
{
    public class SortingHelper
    {
        public static KeyValuePair<string, string>[] GetSortFields()
        {
            return new[] { SortFields.Title, SortFields.CreationDate };
        
        }
      
    }

    public class SortFields
    {
        //                                                          mapujemy na kolumnę w bazie danych o nazwie title
        public static KeyValuePair<string, string> Title { get; } = new KeyValuePair<string, string>("title", "Title"); 
        public static KeyValuePair<string, string> CreationDate { get; } = new KeyValuePair<string, string>("creationdate", "Created");
                                                                                             // tak jak nazywa sie kolumna na bazie danych ...
    }
}
