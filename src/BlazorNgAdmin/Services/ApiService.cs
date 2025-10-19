using BlazorNgAdmin.Models;
using System.Net.Http.Json;

namespace BlazorNgAdmin.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private List<Post>? _cachedPosts;
    private List<User>? _cachedUsers;
    private List<Comment>? _cachedComments;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Posts API calls
    public async Task<List<Post>> GetPostsAsync()
    {
        if (_cachedPosts == null)
        {
            _cachedPosts = await _httpClient.GetFromJsonAsync<List<Post>>("data/posts.json") ?? new List<Post>();
        }
        return _cachedPosts;
    }

    public async Task<Post?> GetPostAsync(int id)
    {
        var posts = await GetPostsAsync();
        return posts.FirstOrDefault(p => p.Id == id);
    }

    public async Task<Post?> CreatePostAsync(Post post)
    {
        var posts = await GetPostsAsync();
        post.Id = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return post;
    }

    public async Task<Post?> UpdatePostAsync(int id, Post post)
    {
        var posts = await GetPostsAsync();
        var existingPost = posts.FirstOrDefault(p => p.Id == id);
        if (existingPost != null)
        {
            existingPost.Title = post.Title;
            existingPost.Body = post.Body;
            existingPost.UserId = post.UserId;
        }
        return existingPost;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var posts = await GetPostsAsync();
        var post = posts.FirstOrDefault(p => p.Id == id);
        if (post != null)
        {
            posts.Remove(post);
            return true;
        }
        return false;
    }

    // Users API calls
    public async Task<List<User>> GetUsersAsync()
    {
        if (_cachedUsers == null)
        {
            _cachedUsers = await _httpClient.GetFromJsonAsync<List<User>>("data/users.json") ?? new List<User>();
        }
        return _cachedUsers;
    }

    public async Task<User?> GetUserAsync(int id)
    {
        var users = await GetUsersAsync();
        return users.FirstOrDefault(u => u.Id == id);
    }

    // Comments API calls
    public async Task<List<Comment>> GetCommentsAsync()
    {
        if (_cachedComments == null)
        {
            _cachedComments = await _httpClient.GetFromJsonAsync<List<Comment>>("data/comments.json") ?? new List<Comment>();
        }
        return _cachedComments;
    }

    public async Task<List<Comment>> GetPostCommentsAsync(int postId)
    {
        var comments = await GetCommentsAsync();
        return comments.Where(c => c.PostId == postId).ToList();
    }
}
