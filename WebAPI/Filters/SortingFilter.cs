using System.Linq;
using WebAPI.Helpers;

namespace WebAPI.Filters
{
    public class SortingFilter
    {
        public string SortField { get; set; }

        public bool Ascending { get; set; } = true;

        public SortingFilter()
        {
            SortField = "Id";// tak jak nazywa sie kolumna na bazie danych ...
        }

        public SortingFilter( string sortField, bool ascending )
        {
            var sortFields = SortingHelper.GetSortFields();

            sortField = sortField.ToLower();
            // teraz jezeli od klienta przyszla prawidlowa nazwa pola czyli title albo creationdate to
            // podmieniamy wartosc zmiennej sort field na wartosc bedaca nazwa pola w bazie dnaych
            // jezeli nie to przypisujemy dopola sort fields wartosc id

            if (sortFields.Select(x => x.Key).Contains(sortField.ToLower())) // albo Title albo CreationDate
                sortField = sortFields.Where(x => x.Key == sortField).Select(x => x.Value).SingleOrDefault(); 
            else
                sortField = "Id"; // tak jak nazywa sie kolumna na bazie danych ...
                                  // na koniec inicjalizujemy:

            SortField = sortField;
            Ascending = ascending;
        }

    }
}
