using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Application.Mappings;
using PYP_Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>, IMapFrom<Category>
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
    }

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
            entity.ImageName=await _fileUpload.FileCreateAsync(request.Photo, "todo", "todo","todo");
            //TODO add root
            entity.CreatedAt=DateTime.Now;
            entity.ModifiedAt = DateTime.Now;
            //TODO add users
            entity.Deleted = false;

            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
