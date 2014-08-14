using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data.Repositories;
using SMN.Services.Tokens;

namespace SMN.Services
{
    public class CheckoutService : ICheckoutService
    {
        private IUserSnapsRepository _userSnapsRepository;

        public CheckoutService(IUserSnapsRepository userSnapsRepository)
        {
            _userSnapsRepository = userSnapsRepository;
        }

        public SnapToken GetSnap(string userID, string id)
        {
            return _userSnapsRepository.Get(userID, id).AsToken();
        }
    }
}
