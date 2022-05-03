using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.Json;

namespace Proxy
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class ProxyService : ProxyServiceI
    {
        // Implémentation de la récupération des contracts
        public string GetContracts()
        {
            ProxyCache<List<Contract>> proxy = new ProxyCache<List<Contract>>();
            List<Contract> result = proxy.Get("Contracts");
            return JsonSerializer.Serialize(result);
        }
        // Implémentation de la récupération des stations
        public string GetStations()
        {
            ProxyCache<Stations> proxy = new ProxyCache<Stations>();
            Stations result = proxy.Get("Stations");
            return JsonSerializer.Serialize(result.stations);
        }

    }
}
