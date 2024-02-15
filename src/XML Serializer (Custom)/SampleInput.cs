using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JSON_Serializer__Custom_;
public class SampleInput
{
    public string Report { get; set; }
    public SampleInput()
    {
        Report = " ";
    }
}

#region Course class
public class Course
{
    public string Title { get; set; }
    public Instructor Teacher { get; set; }
    public List<Topic> Topics { get; set; }
    public double Fees { get; set; }
    public List<AdmissionTest> Tests { get; set; }

    public Course()
    {
        Title = "Asp.net C#";
        Teacher = new Instructor()
        {
            Name = "Md. Jalal Uddin",
            Email = "jalaluddin@devskill.com",
            PermanentAddress = new Address()
            {
                Street = "Moghbazar",
                City = "Dhaka",
                Country = "Bangladesh"
            },
            PresentAddress = new Address()
            {
                Street = "Mirpur-2",
                City = "Dhaka",
                Country = "Bangladesh"
            },
            PhoneNumbers = new List<Phone>
                {
                    new Phone(){ Number = "828320328", Extension = "382", CountryCode = "555" },
                    new Phone(){ Number = "304303343", Extension = "454", CountryCode = "343" },
                }
        };

        Fees = 30000.5;

        Topics = new List<Topic>()
            {
                new Topic
                {
                    Title = "Gettig Started",
                    Description = "Frist Demo",
                    Sessions = new List<Session>
                    {
                        new Session{ DurationInHour = 2, LearningObjective = "Start learning" },
                        new Session{ DurationInHour = 3, LearningObjective = "Write Code" },
                        new Session{ DurationInHour = 4, LearningObjective = "Run Code" },
                    }
                },
                new Topic
                {
                    Title = "Installation",
                    Description = "Tools",
                    Sessions = new List<Session>
                    {
                        new Session{ DurationInHour = 1, LearningObjective = "VS Code" },
                        new Session{ DurationInHour = 4, LearningObjective = "Docker" },
                        new Session{ DurationInHour = 2, LearningObjective = "Git" },
                    }
                },
                new Topic
                {
                    Title = "Project",
                    Description = "Build Application",
                    Sessions = new List<Session>
                    {
                        new Session{ DurationInHour = 2, LearningObjective = "Start learning" },
                        new Session{ DurationInHour = 3, LearningObjective = "Write Code" },
                        new Session{ DurationInHour = 4, LearningObjective = "Run Code" },
                    }
                },
            };

        Tests = new List<AdmissionTest>
            {
                new AdmissionTest
                {
                    TestFees = 100.5,
                    StartDateTime = new DateTime(2022, 2, 3),
                    EndDateTime = new DateTime(2022, 2, 4)
                },
                new AdmissionTest
                {
                    TestFees = 200.5,
                    StartDateTime = new DateTime(2023, 4, 3),
                    EndDateTime = new DateTime(2023, 4, 4)
                },
                new AdmissionTest
                {
                    TestFees = 300.5,
                    StartDateTime = new DateTime(2024, 5, 3),
                    EndDateTime = new DateTime(2024, 5, 4)
                }
            };
    }
}

public class AdmissionTest
{
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double TestFees { get; set; }
}

public class Topic
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Session> Sessions { get; set; }
}

public class Session
{
    public int DurationInHour { get; set; }
    public string LearningObjective { get; set; }
}

public class Instructor
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Address PresentAddress { get; set; }
    public Address PermanentAddress { get; set; }
    public List<Phone> PhoneNumbers { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

public class Phone
{
    public string Number { get; set; }
    public string Extension { get; set; }
    public string CountryCode { get; set; }
}

#endregion

#region Product class
//    public class MyClass
//    {
//        public Guid Id { get; set; }
//        public string Name { get; set; }
//        public string BarCode { get; set; }
//        public string Description { get; set; }
//        public List<Feedback> Feedbacks { get; set; }
//        public Spedification[] Spedificatios { get; set; }
//        public decimal Price { get; set; }
//        public List<Color> Colors { get; set; }

//        public MyClass()
//        {
//            Id = new Guid("931055D0-073C-4423-A010-18D3913E297C");
//            Name = "Camera";
//            Description = "A cannon camera";
//            BarCode = "0475047503";

//            Feedbacks = new List<Feedback>()
//            {
//                new Feedback()
//                {
//                    FeedbackProivdername = "Jalaluddin", Rating = 4.5,
//                    FeedbackItems = new FeedbackItem[]
//                    {
//                        new FeedbackItem() { Rating =  3.2, Name = "Durability"},
//                        new FeedbackItem() { Rating =  3.5, Name = "User Friendliness"},
//                    }
//                },
//                new Feedback()
//                {
//                    FeedbackProivdername = "Tareq", Rating = 2.5,
//                    FeedbackItems = new FeedbackItem[]
//                    {
//                        new FeedbackItem() { Rating = 2.2, Name = "Durability"},
//                        new FeedbackItem() { Rating =  2.5, Name = "User Friendliness"},
//                    }
//                }
//            };

//            Spedificatios = new Spedification[]
//            {
//                new Spedification()
//                {
//                    Items = new List<SpedificationItem>
//                    {
//                        new SpedificationItem() { Name = "Model", Value = "Cannon"},
//                        new SpedificationItem() { Name = "Pixel", Value = "12MPX"}
//                    }
//                }
//            };

//            Price = 30000.5m;

//            Colors = new List<Color>()
//            {
//                new Color() { Name = "FrontColor", Code = "Black"},
//                new Color() { Name = "BackColor", Code = "White"}
//            };
//        }
//    }

//    public class SpedificationItem
//    {
//        public string Name { get; set; }
//        public string Value { get; set; }
//    }

//    public class Spedification
//    {
//        public List<SpedificationItem> Items { get; set; }
//    }

//    public class Feedback
//    {
//        public string FeedbackProivdername { get; set; }
//        public double Rating { get; set; }
//        public FeedbackItem[] FeedbackItems { get; set; }
//    }

//    public class FeedbackItem
//    {
//        public string Name { get; set; }
//        public double Rating { get; set; }
//    }

//    public class Color
//    {
//        public string Name { get; set; }
//        public string Code { get; set; }
//    }
//}
#endregion