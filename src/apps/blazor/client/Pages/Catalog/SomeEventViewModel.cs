using FSH.Starter.Blazor.Infrastructure.Api;


namespace FSH.Starter.Blazor.Client.Pages.Catalog;

public class SomeEventViewModel : UpdateSomeEventCommand
{
    public DateTime? LocalEventDateTime
    {
        get
        {
            // Convert the UTC time from the database to local time for display
            if (EventDate.HasValue)
            {
                return EventDate.Value.ToLocalTime();
            }
            return null;
        }
        set
        {
            // Convert the local time from the UI to UTC for storage
            if (value.HasValue)
            {
                EventDate = value.Value.ToUniversalTime();
            }
            else
            {
                EventDate = null;
            }
        }
    }
    
    public TimeSpan? EventTime
    {
        get
        {
            if (EventDate.HasValue)
            {
                return EventDate.Value.ToLocalTime().TimeOfDay;
            }
            return null;
        }
        set
        {
            if (EventDate.HasValue && value.HasValue)
            {
                // Get the date part from existing EventDate (in UTC)
                var localDate = EventDate.Value.ToLocalTime().Date;
                var localDateTime = localDate.Add(value.Value);
                EventDate = localDateTime.ToUniversalTime();
            }
        }
    }
    
    public DateTime? EventDateOnly
    {
        get
        {
            if (EventDate.HasValue)
            {
                return EventDate.Value.ToLocalTime().Date;
            }
            return null;
        }
        set
        {
            if (EventDate.HasValue && value.HasValue)
            {
                var localTime = EventDate.Value.ToLocalTime().TimeOfDay;
                var localDateTime = value.Value.Add(localTime);
                EventDate = localDateTime.ToUniversalTime();
            }
            else if (value.HasValue)
            {
                // If no time component exists yet, use midnight in local time
                var localDateTime = value.Value.Add(TimeSpan.FromHours(0));
                EventDate = localDateTime.ToUniversalTime();
            }
        }
    }
    
    public void CombineDateAndTime()
    {
        // This method ensures the combined date and time are properly handled
        // The actual conversion happens in the property setters
    }
}
