using Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            #region Title

            RuleFor(x => x.Title).NotEmpty().WithMessage("Post cannot have an empyt title ");
            RuleFor(x => x.Title).Length(5, 100).WithMessage("Title must have between 5 and 100 char");

            #endregion
        }

    }
}
