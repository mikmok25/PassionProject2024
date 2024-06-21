# GamingRig Builder

The GamingRig Builder showcases the use of ASP.NET MVC, WebAPI, and Entity Framework to perform CRUD operations and build a dynamic PC building application.

# Running this project 
1. Ensure the target framework is set correctly:
   - Project > PassionProject2024 Properties > Change target framework to 4.7.1 -> Change back to 4.7.2
2. Create an `App_Data` folder in the project:
   - Right-click solution > View in File Explorer > Create `App_Data` folder if it doesn't exist.
3. Update the database:
   - Tools > Nuget Package Manager > Package Manager Console > `Update-Database`
4. Verify the database is created:
   - View > SQL Server Object Explorer > MSSQLLocalDb > Check for the database.
5. Run API commands through CURL to create new components.

# Common Issues and Resolutions
- **Could not attach .mdf database**: Ensure `App_Data` folder is created.
- **'Type' cannot be null**: Update Entity Framework to the latest version, restart Visual Studio and try again.
- **Exception has been thrown by the target of an invocation**: Clone the project repository to a local drive on the machine instead of a cloud-based storage.
- **Could not find part to the path ../bin/roslyn/csc.exe**: Change target framework to 4.7.1 and back to 4.7.2.
- **System.Web.Http does not have reference to serialize**: Add reference to `System.Web.Extensions`.

- **Hmmm… can't reach this page**: Project > PassionProject2024 Properties > change Project Url to http://localhost:44324/ > Create Virtual Directory

# API Commands
Make sure to utilize `jsondata/component.json` to formulate data you wish to send as part of the POST requests. `{id}` should be replaced with the component's primary key ID. The port number may not always be the same.

### Get a List of Components
```bash 
curl https://localhost:44324/api/componentdata/listcomponents
```

### Get a Single Component
```bash 
curl https://localhost:44324/api/componentdata/findcomponent/{id}
```
### Add a new Component
```bash 
curl -H "Content-Type:application/json" -d @component.json https://localhost:44324/api/componentdata/addcomponent
```

### Update a Component
```bash 
curl -H "Content-Type:application/json" -d @component.json https://localhost:44324/api/componentdata/updatecomponent/{id}
```
# Running the Views for Components

1. You can use SQL Server Object Explorer to add a new Component.
2. You can navigate to **/Component/List** to add a new component.
3. You can navigate to **/Component/Details/{id}** to view component details.
3. You can click edit to update new component.
4. You can click delete to delete the component.


# Running the Views for Builds

1. Navigate to **/Build/List** to see the list of builds.
2. Navigate to **/Build/New** To create new build.


