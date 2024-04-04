using System.Security.Cryptography;

namespace BO;

public class User
{
    public int Id { get; set; }

    public int Password { get; set; }
    public bool IsActive { get; set; }

    public User()
    {
        Id =0;
        Password = 0;
        IsActive = false;
    }

    public override string ToString() => this.ToStringProperty();
}

