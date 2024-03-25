<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>MyApiProject</title>
</head>
<body>

  <h1>MyApiProject</h1>

  <h2>Overview</h2>
  <p>This project is a RESTful API built using C# .NET 6 and PostgreSQL for database storage. Swagger is utilized for API documentation.</p>

  <h2>Usage</h2>
  <p>To open the project, use <code>MyApiProject.sln</code> in Visual Studio.</p>

  <h3>Testing the API</h3>
  <p>The PostgreSQL database is already connected. You can test the API directly by running the project with "Debugg" or pressing F5.</p>

  <h3>Using Your PostgreSQL Database</h3>
  <p>If you want to use your own PostgreSQL database, follow these steps:</p>

  <ol>
    <li><strong>Edit ConnectionStrings</strong>: Open <code>MyApiProject/appsettings.json</code> and edit the connection strings with your database information.</li>
    <li><strong>Run Migrations</strong>:
      <ul>
        <li>Open the Package Manager Console (Tools &gt;&gt; NuGet Package Manager &gt;&gt; Package Manager Console).</li>
        <li>Run the following commands:
          <pre><code>Add-Migration AddContactsTable</code></pre>
          <pre><code>Update-Database</code></pre>
          If you encounter issues, you may need to delete existing migrations using <code>Remove-Migration</code> before running the last two commands.
        </li>
      </ul>
    </li>
    <li><strong>Test API with Your Database</strong>: Once migrations are applied, you can test the API using your PostgreSQL database by running the project with "Debug" or pressing F5.</li>
  </ol>

</body>
</html>
