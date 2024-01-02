using Common.DTOs;
using Common.Model;
using ORM.DatabaseContext;
using RESTCore.GenericRepositoryImplementations;
using System.Reflection;

namespace RESTCore.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserGenericRepo _userGenericRepo;

        public UserService(AppDbContext appDbContext, UserGenericRepo userGenericRepo)
        {
            _appDbContext = appDbContext;
            _userGenericRepo = userGenericRepo;
        }
        public List<UserDTO> GetAllUsers()
        {
            //return _appDbContext.Users.Select(x => new UserDTO
            return _userGenericRepo.GetAll().Select(x => new UserDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                NickName = x.NickName,
                Role = x.Role,
                HireDate = x.HireDate,
                Availability = x.Availability,
                EmailAddress = x.EmailAddress,
                HomePhone = x.HomePhone,
                MobilePhone = x.MobilePhone,
                Id = x.Id,
            }).ToList();
        }

        public UserDTO GetUserById(string id)
        {
            User user = _appDbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.NickName,
                Role = user.Role,
                HireDate = user.HireDate,
                Availability = user.Availability,
                EmailAddress = user.EmailAddress,
                HomePhone = user.HomePhone,
                MobilePhone = user.MobilePhone,
                Id = user.Id,
            };
        }


        public void CreateUser(UserDTO userDTO)
        {
            _appDbContext.Users.Add(new Common.Model.User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                NickName = userDTO.NickName,
                //Role = Enum.Parse<RoleSelection>(userDTO.Role),
                Role = userDTO.Role,
                HireDate = userDTO.HireDate,
                Availability = userDTO.Availability,
                EmailAddress = userDTO.EmailAddress,
                HomePhone = userDTO.HomePhone,
                MobilePhone = userDTO.MobilePhone,
            });
            _appDbContext.SaveChanges();
        }

        public bool UpdateUser(string id, string attributeToUpdate, string newValue)
        {
            User user = _appDbContext.Users.Find(id);

            if (user != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(User).GetProperty(attributeToUpdate, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(user, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool DeleteUserById(string id)
        {
            User user = _appDbContext.Users.Where(x =>
            x.Id == id).SingleOrDefault();

            if (user == null)
            {
                return false;
            }
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
            return true;

        }

    }
}
