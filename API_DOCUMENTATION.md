# API Integration Documentation

## Overview

This Blazor WebAssembly application demonstrates making various HTTP API calls to manage Posts, Users, and Comments. The application uses local JSON files to simulate API endpoints, but the architecture is designed to easily switch to real REST APIs.

## API Service Architecture

The `ApiService` class in `Services/ApiService.cs` is responsible for all HTTP operations. It uses the built-in `HttpClient` with JSON serialization support.

### Key Features

- **Dependency Injection**: ApiService is registered in `Program.cs` and injected into components
- **Async/Await**: All API calls are asynchronous for better performance
- **Error Handling**: Try-catch blocks in components handle API errors gracefully
- **Caching**: In-memory caching to reduce redundant API calls

## API Endpoints

### Posts API

#### GET - Retrieve All Posts
```csharp
public async Task<List<Post>> GetPostsAsync()
```
- **Endpoint**: `data/posts.json`
- **Method**: GET
- **Response**: Array of Post objects
- **Used in**: Posts.razor page to display the list

#### GET - Retrieve Single Post
```csharp
public async Task<Post?> GetPostAsync(int id)
```
- **Endpoint**: `data/posts.json` (filtered by ID)
- **Method**: GET
- **Response**: Single Post object
- **Used in**: PostDetails.razor to show post details

#### POST - Create New Post
```csharp
public async Task<Post?> CreatePostAsync(Post post)
```
- **Method**: POST (simulated in-memory)
- **Request Body**: Post object
- **Response**: Created Post with generated ID
- **Used in**: Posts.razor "Create New Post" button

#### PUT - Update Existing Post
```csharp
public async Task<Post?> UpdatePostAsync(int id, Post post)
```
- **Method**: PUT (simulated in-memory)
- **Request Body**: Updated Post object
- **Response**: Updated Post object
- **Used in**: Posts.razor "Edit" button

#### DELETE - Remove Post
```csharp
public async Task<bool> DeletePostAsync(int id)
```
- **Method**: DELETE (simulated in-memory)
- **Response**: Boolean success indicator
- **Used in**: Posts.razor "Delete" button
- **Effect**: Removes post from the list

### Users API

#### GET - Retrieve All Users
```csharp
public async Task<List<User>> GetUsersAsync()
```
- **Endpoint**: `data/users.json`
- **Method**: GET
- **Response**: Array of User objects
- **Used in**: Users.razor page to display the list

#### GET - Retrieve Single User
```csharp
public async Task<User?> GetUserAsync(int id)
```
- **Endpoint**: `data/users.json` (filtered by ID)
- **Method**: GET
- **Response**: Single User object
- **Used in**: UserDetails.razor to show user details

### Comments API

#### GET - Retrieve All Comments
```csharp
public async Task<List<Comment>> GetCommentsAsync()
```
- **Endpoint**: `data/comments.json`
- **Method**: GET
- **Response**: Array of Comment objects

#### GET - Retrieve Comments for a Post
```csharp
public async Task<List<Comment>> GetPostCommentsAsync(int postId)
```
- **Endpoint**: `data/comments.json` (filtered by postId)
- **Method**: GET
- **Response**: Array of Comment objects for specific post
- **Used in**: PostDetails.razor to show related comments

## Data Models

### Post
```csharp
public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
```

### User
```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
}
```

### Comment
```csharp
public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}
```

## Sample API Calls in Action

### Example 1: Loading Posts
```csharp
@inject ApiService ApiService

protected override async Task OnInitializedAsync()
{
    try
    {
        posts = await ApiService.GetPostsAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading posts: {ex.Message}");
    }
}
```

### Example 2: Deleting a Post
```csharp
private async Task DeletePost(int id)
{
    if (await ApiService.DeletePostAsync(id))
    {
        posts?.RemoveAll(p => p.Id == id);
    }
}
```

### Example 3: Loading Post with Comments
```csharp
protected override async Task OnInitializedAsync()
{
    await LoadPost();
    await LoadComments();
}

private async Task LoadPost()
{
    post = await ApiService.GetPostAsync(Id);
}

private async Task LoadComments()
{
    comments = await ApiService.GetPostCommentsAsync(Id);
}
```

## Converting to Real API

To connect to a real REST API instead of local JSON files:

1. Update `ApiService` constructor to set the base URL:
   ```csharp
   public ApiService(HttpClient httpClient)
   {
       _httpClient = httpClient;
       _httpClient.BaseAddress = new Uri("https://your-api.com");
   }
   ```

2. Replace local file paths with API endpoints:
   ```csharp
   // Before
   await _httpClient.GetFromJsonAsync<List<Post>>("data/posts.json")
   
   // After
   await _httpClient.GetFromJsonAsync<List<Post>>("/api/posts")
   ```

3. Update POST/PUT/DELETE operations to use actual HTTP methods:
   ```csharp
   public async Task<Post?> CreatePostAsync(Post post)
   {
       var response = await _httpClient.PostAsJsonAsync("/api/posts", post);
       return await response.Content.ReadFromJsonAsync<Post>();
   }
   ```

## Error Handling

All API calls include error handling in the components:

```csharp
try
{
    var data = await ApiService.GetPostsAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    // Display user-friendly error message
}
```

## Performance Considerations

- **Caching**: The ApiService caches data to avoid redundant API calls
- **Async Operations**: All API calls are async to prevent UI blocking
- **Loading States**: Components display loading indicators during API calls

## Testing API Calls

To test the API functionality:

1. **List View**: Navigate to `/posts` or `/users` to see GET requests loading data
2. **Detail View**: Click "View" on any item to see individual GET requests
3. **Delete**: Click "Delete" on a post to see the DELETE operation
4. **Related Data**: View a post to see comments loaded from a related endpoint

All operations log to the browser console for debugging purposes.
