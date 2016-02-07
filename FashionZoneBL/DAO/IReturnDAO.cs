using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO
{
    public interface IReturnDAO : IDAO<RETURN>
    {
        /// <summary>
        /// Gets all return motivation.
        /// </summary>
        /// <returns>Listo of D_RETURN_MOTIVATION</returns>
        List<D_RETURN_MOTIVATION> GetAllReturnMotivation();

        /// <summary>
        /// Gets all return motivation by id.
        /// </summary>
        /// <param name="idMot">The id mot.</param>
        /// <returns></returns>
        D_RETURN_MOTIVATION GetAllReturnMotivationById(int idMot);

        /// <summary>
        /// Exists the verification number.
        /// </summary>
        /// <param name="VerificationNumber">The verification number.</param>
        /// <returns>Return 'True' if the verification Number is un use, otherwise 'False'.</returns>
        bool ExistVerificationNumber(string VerificationNumber);
    }
}
