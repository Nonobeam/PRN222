using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository;
using PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Repository.Models;

namespace PE_PRN222_SP25_TrialTest_NguyenHuuPhuc.Service
{
    public interface IMedicineService
    {
        Task<List<MedicineInformation>> GetAllAsync();
        Task<List<MedicineInformation>> GetAllAsync(int pageNumber);
        Task<MedicineInformation?> GetById(string id);
        Task<int> Create(MedicineInformation medicineInformation);
        Task<int> Update(MedicineInformation medicineInformation);
        Task<bool> Delete(MedicineInformation medicineInformation);
        Task<List<MedicineInformation>> Search(string active, string expire, string warn, int pageNumber);
        Task<int> GetTotalCountAsync(string? active, string? expire, string? warn);
    }

    public class MedicineInformationService : IMedicineService
    {
        private readonly MedicineInformationRepository _repository = new();

        public async Task<List<MedicineInformation>> GetAllAsync()
        {
            return await _repository.GetAllMedicinesAsync();
        }

        public async Task<List<MedicineInformation>> GetAllAsync(int pageNumber)
        {
            return await _repository.GetAllMedicinesAsync(pageNumber);
        }

        public async Task<MedicineInformation?> GetById(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<MedicineInformation>> Search(string active, string expire, string warn, int pageNumber)
        {
            return await _repository.Search(active, expire, warn, pageNumber);
        }

        public async Task<int> Create(MedicineInformation medicineInformation) => await _repository.CreateAsync(medicineInformation);

        public async Task<int> Update(MedicineInformation medicineInformation) => await _repository.UpdateAsync(medicineInformation);

        public async Task<bool> Delete(MedicineInformation medicineInformation) => await _repository.RemoveAsync(medicineInformation);

        public async Task<int> GetTotalCountAsync(string? active, string? expire, string? warn) => await _repository.GetTotalCountAsync(active, expire, warn);
    }
}
