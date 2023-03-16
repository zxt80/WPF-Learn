using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.api.Context;
using MyToDo.shared;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;
using System.Reflection.Metadata;

namespace MyToDo.api.Service
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            try
            {
                var memo = _mapper.Map<Memo>(model);
                await _unitOfWork.GetRepository<Memo>().InsertAsync(memo);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, memo);
                return new ApiResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(memo);

                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameters parameter)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Memo>();
                var memos = await repository.GetPagedListAsync(predicate:
                x => string.IsNullOrEmpty(parameter.Search) ? true : x.Title.Equals(parameter.Search),
                pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateTime));
                return new ApiResponse(true, memos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, memo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            try
            {
                var memoOld = _mapper.Map<Memo>(model);
                var repository = _unitOfWork.GetRepository<ToDo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                memo.Title = memoOld.Title;
                memo.Content = memoOld.Content;
                memo.UpdateTime = DateTime.Now;
                repository.Update(memo);

                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, memo);
                return new ApiResponse("更新数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }
    }
}
