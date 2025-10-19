# Blazor Admin Panel

A Blazor WebAssembly application inspired by ng-admin, providing a modern admin interface for managing data with RESTful API integration.

## Features

✅ **RESTful API Integration** - Makes HTTP GET, POST, PUT, and DELETE requests  
✅ **CRUD Operations** - Complete Create, Read, Update, and Delete functionality  
✅ **Data Management** - List views with tables for Posts and Users  
✅ **Detail Views** - Individual resource pages with related data (e.g., post comments)  
✅ **Responsive Design** - Built with Bootstrap for mobile-friendly layouts  
✅ **Clean Navigation** - Intuitive routing and navigation structure  

## Screenshots

### Home Page
![Home Page](https://github.com/user-attachments/assets/a56a3a36-6b82-4950-a598-1db940c549b7)

### Posts Management
![Posts List](https://github.com/user-attachments/assets/73ab0a34-da8e-4156-9d95-9f0dd9ed992e)

### Post Details with Comments
![Post Details](https://github.com/user-attachments/assets/c64e6402-54d4-4f67-8d4a-0c6c10c6270f)

### Users Management
![Users List](https://github.com/user-attachments/assets/6ced1068-3e85-44fc-b331-f4d08dbb6600)

### User Details
![User Details](https://github.com/user-attachments/assets/b026a1af-9d0b-4280-9d31-056ad065c07a)

## Technology Stack

- **Framework**: Blazor WebAssembly (.NET 9.0)
- **Language**: C#
- **UI Library**: Bootstrap 5
- **Data Format**: JSON

## Project Structure

```
src/BlazorNgAdmin/
├── Models/              # Data models (Post, User, Comment)
├── Services/            # API service layer
├── Pages/              # Razor components for pages
├── Layout/             # Layout components and navigation
└── wwwroot/            
    └── data/           # Sample JSON data files
```

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later

### Running the Application

1. Clone the repository
   ```bash
   git clone https://github.com/kschipulring/blazor-ng-admin.git
   cd blazor-ng-admin
   ```

2. Navigate to the project directory
   ```bash
   cd src/BlazorNgAdmin
   ```

3. Run the application
   ```bash
   dotnet run
   ```

4. Open your browser and navigate to `http://localhost:5000`

### Building for Production

```bash
dotnet publish -c Release
```

The published files will be in `bin/Release/net9.0/publish/wwwroot/`

## API Endpoints

The application demonstrates the following API operations:

### Posts
- `GET /data/posts.json` - Retrieve all posts
- `GET /data/posts.json` (filtered by ID) - Get a single post
- `POST` (simulated) - Create a new post
- `PUT` (simulated) - Update an existing post
- `DELETE` (simulated) - Delete a post

### Users
- `GET /data/users.json` - Retrieve all users
- `GET /data/users.json` (filtered by ID) - Get a single user

### Comments
- `GET /data/comments.json` - Retrieve all comments
- `GET /data/comments.json` (filtered by post ID) - Get comments for a post

*Note: The application uses local JSON files for demonstration. In a production environment, these would be replaced with actual API endpoints.*

## Components

### Pages
- **Home** - Landing page with feature overview
- **Posts** - List all posts with CRUD operations
- **PostDetails** - View individual post with comments
- **Users** - List all users
- **UserDetails** - View individual user information

### Services
- **ApiService** - Handles all HTTP operations for data fetching and manipulation

### Models
- **Post** - Blog post model with ID, UserId, Title, and Body
- **User** - User model with profile information
- **Comment** - Comment model linked to posts

## Development

The application follows best practices for Blazor development:

- Dependency injection for services
- Async/await patterns for API calls
- Component-based architecture
- Separation of concerns (Models, Services, Pages)

## License

This project is open source and available under the MIT License.

## Acknowledgments

- Inspired by [ng-admin](https://github.com/marmelab/ng-admin) - An AngularJS framework for REST APIs
- Sample data structure based on [JSONPlaceholder](https://jsonplaceholder.typicode.com/)
