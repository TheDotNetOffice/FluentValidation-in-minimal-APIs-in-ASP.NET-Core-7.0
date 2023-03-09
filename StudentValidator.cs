using FluentValidation;

namespace FluentValidationInAspCore
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(15);
            RuleFor(x => x.Age).NotNull().InclusiveBetween(5,20);
            RuleFor(x => x.Address).NotNull().LessThanOrEqualTo(200);
        }
    }
}
