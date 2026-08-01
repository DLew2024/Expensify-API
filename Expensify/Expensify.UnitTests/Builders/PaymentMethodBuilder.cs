using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;

namespace Expensify.UnitTests.Builders
{
    public sealed class PaymentMethodBuilder
    {
        private Guid _id = Guid.NewGuid();

        private Guid? _userId;
        private User? _user;

        private string _name = "Test Payment Method";
        private string _description = "Test Payment Method Description";
        private bool _isActive = true;
        private bool _isSystemDefault = false;
        private bool _isDeleted = false;

        public PaymentMethodBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public PaymentMethodBuilder WithUser(User user)
        {
            _user = user;
            _userId = user.Id;
            return this;
        }

        public PaymentMethodBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PaymentMethodBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public PaymentMethodBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public PaymentMethodBuilder WithIsSystemDefault(bool isSystemDefault)
        {
            _isSystemDefault = isSystemDefault;
            return this;
        }

        public PaymentMethodBuilder WithDeletedStatus(bool IsDeleted)
        {
            _isDeleted = IsDeleted;
            return this;
        }

        public PaymentMethod Build()
        {
            return new PaymentMethod
            {
                Id = _id,
                Name = _name,
                User = _user,
                UserId = _userId,
                Description = _description,
                IsActive = _isActive,
                IsDeleted = _isDeleted,
                IsSystemDefault = _isSystemDefault,
            };
        }
    }
}
