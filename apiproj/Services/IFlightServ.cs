using apiproj.Models;
using apiproj.Repository;

namespace apiproj.Services
{

    public interface IFlightServ<HetalFlight>
    {
        List<HetalFlight> ShowFlights();
        void AddFlight(HetalFlight flight);
        void EditFlight(int id, HetalFlight flight);
        HetalFlight GetFlightById(int id);
        void DeleteFlight(int id);

        string Message(string name)
        {
            return "Hello "+name;
        }
    }
}