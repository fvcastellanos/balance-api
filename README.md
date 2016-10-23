# Balance API

This is an API that is aimed to provide a service to help people to control their saving and monetary accounts, control their expenses.

Right now it is just the first steps, but hopefully it will grow soon.

It is done using ASP.NET 5 (we wanted to try the multi platform approach that Microsoft claims that this new version of ASP has)

## Test

```ssh

http://localhost:5000/api/values

```

Test end point

## Database Migrations

To run SQL scripts (database migrations) [flyway db]https://flywaydb.org/documentation/commandline/ was used, would be great if we have a .net port of this great tool

### Migrating

Just go to ```ssh Migrations ``` directory and execute ```ssh migrate.sh ``` script

```ssh
cd Migrations
./migrate.sh
```

### Cleaning db

Just go to ```ssh Migrations ``` directory and execute ```ssh clean.sh ``` script

```ssh
cd Migrations
./clean.sh
```

