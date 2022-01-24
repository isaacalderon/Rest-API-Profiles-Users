# Rest-API-Profiles-Users
CRUD Rest API with the following models:
![image](https://user-images.githubusercontent.com/96512191/150863227-680e9d3b-d320-49de-a132-99e2740d8c74.png)

## Profile
### Create Profile
POST /api/Profiles
Body Example:
{
  "id": 1,
  "name": "Joe"
}

### Read Profiles
GET /api/Profiles

### Read Profile
GET /api/Profile

### Update Profile
PUT /api/Profiles/{id}
Body Example:
{
  "id": 1,
  "name": "Bob"
}

### Delete Profile
DELETE /api/Profiles/{id}

## User
### Create User
POST /api/Users
Body Example:
{
  "id": 0,
  "userName": "JohnDoe",
  "password": "Password123",
  "profileId": 1,
  "idEmployee": 1,
  "status": "Active",
  "login": "johndoe123"
}

### Read Users
GET /api/Users

### Read User
GET /api/Users/{id}

### Update User
PUT /api/Users/{id}
Body Example:
{
  "id": 1,
  "userName": "Bob",
  "password": "Pasword321",
  "profileId": 2,
  "idEmployee": 2,
  "status": "Inactive",
  "login": "bob123"
}

### Delete Users
DELETE /api/Users/{id}
