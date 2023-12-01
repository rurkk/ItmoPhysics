using ItmoPhysics.Data;
using ItmoPhysics.Servicies.MatrixValidators.Results;
using ItmoPhysics.Servicies.MatrixValidators;
using System.ComponentModel.DataAnnotations;

namespace ItmoPhysics.Servicies.MatrixInfoUpload.UserFileUploads
{
    public class UserFileUploadBuilder : IUserFileUploadBuilder
    {
        private byte[]? _data;
        private readonly ApplicationContext _context;
        public UserFileUploadBuilder(ApplicationContext context)
        {
            _context = context;
        }
        public IUserFileUpload Build()
        {
            if (_data is null) throw new InvalidOperationException(nameof(Build));

            return new UserFileUpload(_data, _context);
        }

        public void WithData(byte[] data)
        {
            _data = data;
        }
    }
}
