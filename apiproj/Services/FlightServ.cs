using apiproj.Models;
using apiproj.Repository;

namespace apiproj.Services
{

    public class FlightServ : IFlightServ<HetalFlight>
    {

        private readonly IFlight<HetalFlight> flightrepo;
        public FlightServ(){}

        public FlightServ(IFlight<HetalFlight> _flightrepo)
        {
            flightrepo = _flightrepo;
        }
        public void AddFlight(HetalFlight flight)
        {
           flightrepo.AddFlight(flight);
        }
        public void DeleteFlight(int id)
        {
            flightrepo.DeleteFlight(id);
        }
        public List<HetalFlight> ShowFlights()
        {
            return flightrepo.ShowFlights();
        }

        public HetalFlight GetFlightById(int id)
        {
            return flightrepo.GetFlightById(id);
        }

        public void EditFlight(int id, HetalFlight flight)
        {
            flightrepo.EditFlight(id,flight);
        }
    }
}