using apiproj.Models;

namespace apiproj.Repository
{

    public class FlightRepo : IFlight<HetalFlight>
    {
        private readonly Ace52024Context ctx;
        public FlightRepo(){}

        public FlightRepo(Ace52024Context _ctx)
        {
            ctx=_ctx;
        }
        public void AddFlight(HetalFlight flight)
        {
            ctx.HetalFlights.Add(flight);
            ctx.SaveChanges();
        }

        public void DeleteFlight(int id)
        {
            HetalFlight flight = ctx.HetalFlights.Find(id);
            ctx.HetalFlights.Remove(flight);
            ctx.SaveChanges();
        }

        public List<HetalFlight> ShowFlights()
        {
            return ctx.HetalFlights.ToList();
        }

        public HetalFlight GetFlightById(int id)
        {
            return  ctx.HetalFlights.Find(id);
        }

        public void EditFlight(int id, HetalFlight flight)
        {
            ctx.HetalFlights.Update(flight);
            ctx.SaveChanges();

        }
    }
}