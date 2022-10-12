using AutoMapper;
using MediatR;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;

namespace PYP_Book.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUpload;

        public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper,IFileUploadService fileUpload)
        {
            _repository = repository;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Category>(request);
            entity.ImageUrl=await _fileUpload.FileCreateAsync(request.Image);
            await _repository.AddAsync(entity,cancellationToken);
            return entity.Id;
        }
    }
}
