# A fork of the original using the system default security protocol (compatible with TLS 1.2)
---------------------------------------------------------------------------------------------

### Installation
The binary can be found under the releases section. [Latest release](https://github.com/SBentley/QlikSense-Task-Failure-Email-Alerts/releases/tag/v1.0.0)

## Changes
 A small tweak on the [original](https://github.com/NickAkincilar/QlikSense-Task-Failure-Email-Alerts) to use the default system security protocol, this ensures it can communicate with Qlik using TlS 1.2 and future versions.

 Other changes
 * Test connection button on config window now shows how many failed and successful tasks it found. It no longer succeeds silently if no failed tasks were found but successful ones were.
 * New button on config window to test email and API at the same time. This will hopefully give you more confidence that everything is set up right
 * _Slightly_ increased code quality
 * Put code into a git friendly structure (not zip files)
 * Bumped framework version to .NET Framework 4.7

### Building the project
 In order to bulid the installer project using Visual Studio 2019 or later you will need to install the [Visual Studio Installer Project Extension](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects)
