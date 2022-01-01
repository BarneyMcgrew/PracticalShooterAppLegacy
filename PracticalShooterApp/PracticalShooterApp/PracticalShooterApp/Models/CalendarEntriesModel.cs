using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace PracticalShooterApp.Models
{
    [DataContract]
    public class CalendarEntriesModel
    {
        [DataMember(Name = "calendarEventType")]
        [JsonProperty(PropertyName = "calendarEventType")]
        public String CalendarEventType { get; set; }

        [DataMember(Name = "calendarId")]
        [JsonProperty(PropertyName = "calendarId")]
        public String CalendarId { get; set; }

        [DataMember(Name = "eventName")]
        [JsonProperty(PropertyName = "eventName")]
        public String EventName { get; set; }

        [DataMember(Name = "disciplineName")]
        [JsonProperty(PropertyName = "disciplineName")]
        public String DisciplineName { get; set; }

        [DataMember(Name = "associationName")]
        [JsonProperty(PropertyName = "associationName")]
        public String AssociationName { get; set; }

        [DataMember(Name = "matchLevel")]
        [JsonProperty(PropertyName = "matchLevel")]
        public String MatchLevel { get; set; }

        [DataMember(Name = "eventCountry")]
        [JsonProperty(PropertyName = "eventCountry")]
        public String EventCountry { get; set; }

        [DataMember(Name = "eventDescription")]
        [JsonProperty(PropertyName = "eventDescription")]
        public String EventDescription { get; set; }

        [DataMember(Name = "eventLocation")]
        [JsonProperty(PropertyName = "eventLocation")]
        public String EventLocation { get; set; }

        [DataMember(Name = "eventImageUrl")]
        [JsonProperty(PropertyName = "eventImageUrl")]
        public String EventImageUrl { get; set; }

        [DataMember(Name = "eventStartDate")]
        [JsonProperty(PropertyName = "eventStartDate")]
        public DateTime EventStartDate { get; set; }

        [DataMember(Name = "eventEndDate")]
        [JsonProperty(PropertyName = "eventEndDate")]
        public DateTime EventEndDate { get; set; }

        [DataMember(Name = "calendarUrl")]
        [JsonProperty(PropertyName = "calendarUrl")]
        public String CalendarUrl { get; set; }

        public String InlineDisplayName { get { return $"{EventName} | {EventLocation}"; } }

        public String InlineIdentifier { get { return $"{InlineDisplayName} | {EventStartDate}"; } }

        public CalendarInlineEvent ToInlineEvent()
        {
            return new CalendarInlineEvent()
            {
                Subject = InlineDisplayName,
                StartTime = EventStartDate,
                EndTime = EventEndDate,
                Color = GetEventTypeColor(CalendarEventType)
            };
        }

        private Color GetEventTypeColor(String calendarEventType)
        {
            switch (calendarEventType)
            {
                default:
                    return (Color)Application.Current.Resources["Tertiary"];
            }
        }
    }
}