using MediatR;
using Microsoft.AspNetCore.Http;
using PYP_Book.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _repository;
        private readonly IFileUploadService _fileUpload;

        public UpdateBookCommandHandler(IBookRepository repository,IFileUploadService fileUpload)
        {
            _repository = repository;
            _fileUpload = fileUpload;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new ArgumentException();
                //throw new NotFoundException("UpdateBookCommand");
                //throw new NotFoundException(nameof(DeleteBookCommand), request.Id);
            }

            entity.Name = request.Name;
            if (request.Image!=null)
            {
               entity.ImageUrl=await _fileUpload.FileCreateAsync(request.Image);
            }
            await _repository.UpdateAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}
