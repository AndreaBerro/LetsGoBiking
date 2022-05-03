using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Proxy
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface ProxyServiceI
    {
        // Interface de la récupération des contracts
        [OperationContract]
        string GetContracts();

        // Interface de la récupération des stations
        [OperationContract]
        string GetStations();
    }
}
