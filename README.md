# Mini Pinterest
Web application that serves as a virtual pinboard, allowing users to discover, save, and organize content based on their interests. With its visually appealing interface, users can explore a vast array of images posted by other users. They can create their own boards to save and categorize content. With MiniPinterest, users can spark creativity, find inspiration, and curate their digital collections in an intuitive and visually captivating manner.

### Current Features Include
- CRUD operations on pins and boards: Users can create, read, update, and delete pins and boards, allowing them to manage their collections effectively.

- Uploading images: Users can upload images to be associated with their pins, enabling them to personalize their content and share visual representations.

- Logging: The application includes a logging system, which allows users to sign in to their accounts and access personalized features and data.

- Scrolling through other users' pins: Users have the ability to browse and view pins created by other users, facilitating inspiration and discovery of new content.

- Saving pins on boards: Users can save pins to their own boards, enabling them to organize and categorize content based on their preferences and interests.

- Pins likes and comments: Users can express their appreciation for pins by liking or commenting them, providing a way to interact with and show support for the content created by others.

## Architecture
- MVC
- ViewModel pattern
- Repository pattern
- Dependency Injection
- Entity - Model mapping

## Frameworks and tools
- ASP.NET Core MVC
- ORM: EntityFramework Core
- Users Management: ASP.NET Core Identity

## How to run
- clone repository
- navigate to the cloned repository's directory
- install required dependencies by executing "dotnet restore"
- create databases
- create an account on cloudinary
- add an appsettings.json file with connection strings to your own databases and infromation about your cloudinary hosting:
![image](https://github.com/natiniedzialek/MiniPinterest/assets/76866602/ef7065da-a8b7-4d6b-b4c2-c25bbc978b99)
- run project "dotnet watch run Program.cs"
