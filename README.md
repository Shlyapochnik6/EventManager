<h1>Instructions for running the application</h1>
<h2>Running the project through an IDE</h2>
<table>
    <tr>
      <td>1.</td>
      <td>
        <p>Clone the repository to a local directory</p>
      </td>
    </tr>
    <tr>
      <td>2.</td>
      <td>
        <p>Update the connection string in the <code>appsettings.json</code> file to point to your PostgreSQL database:</p>
        <pre><code>"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=EventManager;Username=[username];Password=[password]"
  }</code></pre>
      </td>
    </tr>
    <tr>
      <td>3.</td>
      <td>
        <p>Go to the EventManager.EventManager.Persistence directory and run the database migrations using the following command:</p>
        <pre><code>dotnet ef database update</code></pre>
      </td>
    </tr>
    <tr>
      <td>4.</td>
      <td>
        <p>Launch the project on port HTTP or HTTPS and navigate to the Swagger UI to test the API endpoints</p>
      </td>
    </tr>
    <tr>
</table>