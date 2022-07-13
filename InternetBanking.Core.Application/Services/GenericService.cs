using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        private readonly IGenericRepository<Entity> _repo;
        private readonly IMapper _mapper;
        public GenericService(IGenericRepository<Entity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            entity = await _repo.AddAsync(entity);
            SaveViewModel viewModel = _mapper.Map<SaveViewModel>(entity);
            return viewModel;
        }

        public virtual async Task Delete(int id)
        {
            Entity entity = await _repo.GetByIdAsync(id);
            await _repo.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllVm()
        {
            var entityList = await _repo.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entityList);
        }

        public virtual async Task<SaveViewModel> GetbyIdVM(int id)
        {
            Entity entity = await _repo.GetByIdAsync(id);
            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public virtual async Task Update(SaveViewModel vm, int id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repo.UpdateAsync(entity, id);
        }
    }
}
