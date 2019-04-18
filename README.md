# FixMensIntegration
Api for integration Firebird to SQL Server Azure

Files ignored (create them before publish)
PrivateSettings.config
Example:
<appSettings>
  <add key="MAIL_PASSWORD" value="xspbqmurkjadteck"/>
</appSettings>

connectionStrings.config
Example:
<connectionStrings>
  <add name="FixmensModel" connectionString="data source=myserver.host;initial catalog=mydb;persist security info=True;user id=talleronline;password=mypassword;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>