# User secrets

This test program can read settings from user secrets. Using user secrets are recommended to avoid accidentally adding username and password to the git repository when performing tests locally. An application using the library is expected to complement the configuration object with values from a key vault.

Change the dummy user name and password in the commands below and run them in the ClientTester project folder.

```
dotnet user-secrets set "NotificationSettings:Username" "dummy"
dotnet user-secrets set "NotificationSettings:Password" "dummy"
```
