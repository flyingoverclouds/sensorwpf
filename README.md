#Old code migrated from Codeplex - some stuffs may be obsolete.

# sensorwpf
WPF Wrapper for Windows 7 Sensor API (Codeplex migration)

This project provide an "wpf compatible" encapsulation of the Windows7 Sensor API.
Based on the .net sample from Microsoft.
A sample game is provided (like "Tron").

The SensorWpf project implement the Wpf way of programming to expose Event, Datas and properties, allowing WPF application to drasticly simplified the use of sensors.

The API is based on the .net sample code provided by Microsoft, thus you need to download and build it separatly (http://code.msdn.microsoft.com/SensorsAndLocation), then update the reference in the projects to target the built dll.

You need Windows 7 RC with a Visual Studio 2008 to compile and run this code.
http://technet.microsoft.com/fr-fr/evalcenter/dd353205.aspx
http://msdn.microsoft.com/fr-fr/evalcenter/bb655861.aspx

If you really want to test this project, you need and hardware with an 3D accelerometer and sensitive switch array.
All the code and test has been done using the "Windows Sensor Development Platform" provided by Microsoft during PDC2008. If you want to buy one, please go to the Freescale website http://www.freescale.com/webapp/sps/site/prod_summary.jsp?code=JMBADGE2008-B.

This project has been develloped and tested using the Windows Sensor Developement Platform. Some very little part of code is specialized for this platform. I could'nt test is with other hardware.

The Tron game is a modified version of the original code provided by Mitsu Furuta http://blogs.msdn.com/mitsufu/(french DPE microsoftee) during Techdays 2007. The code has been modified and published with his agreement.

See https://nicolasclerc.wordpress.com/2009/07/03/sensorwpf-une-encapsulation-pour-wpf-de-l’api-sensor-de-windows-7/ 
