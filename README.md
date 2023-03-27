<h1>Instructions for running the application:</h1>
<hr>
<p>There are two ways to run the web application. 
    The first is through the IDE, and the second is through Docker Compose</p>
<h2>The first way to start the project</h2>
<hr>
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
        <p>Create a PostgreSQL database with a name of your choice</p>
      </td>
    </tr>
    <tr>
      <td>3.</td>
      <td>
        <p>Update the connection string in the <code>appsettings.json</code> file to point to your PostgreSQL database:</p>
        <pre><code>"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=[database-name];Username=[username];Password=[password]"
  }</code></pre>
      </td>
    </tr>
    <tr>
      <td>4.</td>
      <td>
        <p>Go to the EventManager.Persistence directory and run the database migrations using the following command:</p>
        <pre><code>dotnet ef database update</code></pre>
      </td>
    </tr>
    <tr>
      <td>5.</td>
      <td>
        <p>Launch the project and navigate to the Swagger UI to test the API endpoints</p>
      </td>
    </tr>
    <tr>
</table>
<h2>The second way to start the project (via Docker Compose)</h2>
<hr>
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
        <p>Navigate to the directory containing the docker-compose.yml file and run the following command:</p>
        <pre><code>docker-compose up</code></pre>
      </td>
    </tr>
    <tr>
      <td>3.</td>
      <td>
        <p>You should be able to access the ASP.NET Core application in your browser at http://localhost:8000/swagger/index.html</p>
      </td>
    </tr>
</table>
