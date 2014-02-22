using System.Collections.Generic;
using System.Web.Http;
using Messages;
using Rebus;

namespace SignalRExample.Api
{
    public class NotificationController : ApiController
    {
        public IBus Bus { get; set; }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            Bus.Send(new GlobalNotificationMessage {Message = value, Title = "Global Message"});
        }
    }
}