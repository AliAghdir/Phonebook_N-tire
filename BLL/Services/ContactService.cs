using BLL.Dto;
using DAL.DataBase;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    //This method is a module table
    public class ContactService
    {
        DataBaseContext context = new DataBaseContext();

        public List<ContactListDto> GetContactList(string key = null)

        {
            var contactQuery = context.Contacts.AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                contactQuery = contactQuery.Where(p => p.Name.Contains(key)
                                            || p.LastName.Contains(key)
                                            || p.Company.Contains(key)
                                            || p.PhoneNumber.Contains(key));
            }
            
            var ContactsList =contactQuery.Select(p => new ContactListDto
            {
                Id=p.Id,
                FullName = $"{p.Name} {p.LastName}",
                PhoneNumber = p.PhoneNumber
            }).ToList();

            return ContactsList;
        }
        
        public ResultDto DeleteContact(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact!=null)
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "مخاطب با موفقیت حذف شد"
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "مخاطب یافت نشد"
                };
            }
        }

        public ResultDto<ContactDetailsDto> GetContactDetails(int id)
        {
            var contactDitails = context.Contacts.Find(id);
            if (contactDitails!=null)
            {
                var data = new ContactDetailsDto
                {
                    Id = contactDitails.Id,
                    Name = contactDitails.Name,
                    LastName = contactDitails.LastName,
                    Company = contactDitails.Company,
                    CreateAt = contactDitails.CreateAt,
                    PhoneNumber = contactDitails.PhoneNumber,
                    Description = contactDitails.Description
                };
                return new ResultDto<ContactDetailsDto>
                {
                    IsSuccess = true,
                    Message = "",
                    Data = data
                };
            }
            
            return new ResultDto<ContactDetailsDto>
            {
                IsSuccess = false,
                Message = "اطلاعات مخاطب یافت نشد",
                Data = null
            };
        }

        public ResultDto AddNewContact(AddNewContactDto NewContact)
        {
            if (string.IsNullOrEmpty(NewContact.Name) || string.IsNullOrEmpty(NewContact.PhoneNumber))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نام یا شماره تلفن نمیتواند خالی باشد"
                };
            }
            Contact contact = new Contact()
            {
                Name = NewContact.Name,
                LastName = NewContact.LastName,
                PhoneNumber = NewContact.PhoneNumber,
                Company = NewContact.Company,
                Description = NewContact.Description,
                CreateAt = DateTime.Now
            };
            context.Contacts.Add(contact);
            context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = $"با موفقیت ثبت شد {contact.Name} مخاطب"
            };
        }

        public ResultDto EditContact(EditContactDto editContactDto)
        {
            if (string.IsNullOrEmpty(editContactDto.Name) || string.IsNullOrEmpty(editContactDto.PhoneNumber))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نام یا شماره تلفن نمیتواند خالی باشد"
                };
            }

            var contact = context.Contacts.Find(editContactDto.Id);
            if (context==null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "مخاطب یافت نشد"
                };
            }

            contact.Name = editContactDto.Name;
            contact.LastName = editContactDto.LastName;
            contact.PhoneNumber = editContactDto.PhoneNumber;
            contact.Company = editContactDto.Company;
            contact.Description = editContactDto.Description;
            
            context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = $"با موفقیت ثبت شد {contact.Name} مخاطب"
            };
        }
    }
}
