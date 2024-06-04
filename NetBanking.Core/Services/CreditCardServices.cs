using Microsoft.Extensions.Options;
using NetBanking.Core.CustomEntitys;
using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Core.Interfaces.Services;
using NetBanking.Core.Options;
using NetBanking.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
	public class CreditCardServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : ICreditCardServices
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly PaginationOptions paginationOptions = paginationOptions.Value;

		public async Task IssuerUserValidations(CreditCard creditCard)
		{
			var issuerUser = await _unitOfWork.UserRepository.GetByIdAsync(creditCard.UserId);
			if (issuerUser == null)
				throw new BusinessLogicException($"The User with id:{creditCard.UserId} doesnt exists");

			if (issuerUser.UserStatus != UserStatus.active)
				throw new BusinessLogicException($"The User: {issuerUser.UserName} does not have a status required");

		}

		public async Task AddAsync(CreditCard creditCard)
		{
			await IssuerUserValidations(creditCard);

			try
			{
				await _unitOfWork.CreditCardRepository.AddAsync(creditCard);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}

		}

		public async Task DeleteAsync(int creditCardId)
		{
			try
			{
				await _unitOfWork.CreditCardRepository.DeleteAsync(creditCardId);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public PagedList<CreditCard> GetAll(CreditCardQueryServices filters)
		{
			var creditCards = _unitOfWork.CreditCardRepository.GetAllWithEagerLoding(x=>x.User);

			filters.CurrentPage = (filters.CurrentPage == 0 ? paginationOptions.CurrentPage : filters.CurrentPage);
			filters.PageSize = (filters.PageSize == 0 ? paginationOptions.PageSize : filters.PageSize);

			if (filters.IdentifierNumber != null)
				creditCards = creditCards.Where(x => x.IdentifierNumber == filters.IdentifierNumber);


			if (filters.UserId != null)
				creditCards = creditCards.Where(x => x.UserId == filters.UserId);

			if (filters.ProductType != null)
				creditCards = creditCards.Where(x => x.ProductType == filters.ProductType);

			if (filters.ProductStatus != null)
				creditCards = creditCards.Where(x => x.ProductStatus == filters.ProductStatus);

			return PagedList<CreditCard>.Create(creditCards, filters.CurrentPage, filters.PageSize);
		}

		public async Task<CreditCard?> GetByIdAsync(int creditCardId)
		{
			return await _unitOfWork.CreditCardRepository.GetByIdWithEagerLodingAsync(creditCardId, x=>x.User);
		}

		public async Task UpdateAsync(CreditCard creditCard)
		{
			await IssuerUserValidations(creditCard);

			try
			{
				await _unitOfWork.CreditCardRepository.UpdateAsync(creditCard);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}
	}
}
