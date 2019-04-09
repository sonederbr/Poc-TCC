using System;

namespace PocTcc.Models.Entities
{
  public class Customer
  {
    public Customer()
    {
      Identity = new AppUser();
    }
    public Customer(string identityId, AppUser identity, string location, string locale, string gender)
    {
      Id = Guid.NewGuid().ToString();
      IdentityId = identityId;
      Identity = identity;
      Location = location;
      Locale = locale;
      Gender = gender;
    }
    public string Id { get; set; }
    public string IdentityId { get; set; }
    public AppUser Identity { get; set; }  // navigation property
    public string Location { get; set; }
    public string Locale { get; set; }
    public string Gender { get; set; }
  }
}
