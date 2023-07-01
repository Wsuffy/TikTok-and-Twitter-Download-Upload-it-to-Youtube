# **TikTok/Twitter Video Downloader and YouTube Uploader**
![Last Version](https://img.shields.io/badge/release-1.0.1-green)
![Was Made Icon](https://img.shields.io/badge/Was_Made_With-Wsuffy-blue)
![License](https://img.shields.io/badge/License-MIT-green)

This GitHub project is an application that simplifies the process of downloading TikTok and Twitter videos with just one click, and then uploading them to YouTube.
Additionally, the application provides a loading history feature that logs the progress of your uploads in a JSON file, presenting it in a visually appealing manner for easy reference.
Overall, this GitHub project aims to simplify the download of TikTok and Twitter videos and streamline the uploading process to YouTube, ensuring a seamless experience for users.

# Download ***[(#EXE is for you)](https://disk.yandex.ru/d/xCUo1tVHvAIDPw)***
1. Just download EXE folder
2. Make sure you have Node.js installed
3. Install the playwright using the "npm init playwright@latest"
4. Run application

# Usage
The application provides a simple and intuitive user interface for downloading Tik Tok and Twitter videos and uploading them to YouTube. Follow the steps below to use the application effectively:
Launch the application after successful installation.
Enter the URL of the TikTok or Twitter video you want to download in the provided input field.
Click the "Download" button to initiate the download process. The application will retrieve the video without any watermarks and store it in a specified location on your computer.
Once the download is complete, you can select the downloaded video from the file explorer.
Provide the necessary details, such as video title, description, and tags, in the YouTube upload window.
Click the "Upload" button to begin the uploading process. The application will utilize the Playwright library to upload the video to YouTube without using the YouTube API.

## Important notes
When your open application for the first time make it create needed files ***wait about 5 second*** and then use input box to fill your information, and the direcory ***if you fill the input fields correct for the 1 time it will be no need to fill it againg until you want change youtube account***.
***Don't forget to fill tittle and discription fields - it may lead you to block the video on youtube or even Exception***.
If you have recently created your YouTube account, you may need to navigate through certain pop-ups such as enabling dark theme and dismissing welcome greetings specific to YouTube. The application will guide you through this process.



# Architecture
The project follows the Onion architecture pattern, which promotes a separation of concerns and emphasizes the use of domain-driven design principles. The architecture is structured in layers, with the core domain at the center and external dependencies on the outer layers. The layers of the Onion architecture include:

Core: Contains the business logic and domain models.
Application: Implements use cases and orchestrates interactions between the core and infrastructure layers.
Infrastructure: Handles external concerns such as data access, web APIs, and file system operations.
Presentation: Deals with user interface and interaction.
The Onion architecture promotes modularity, testability, and maintainability by enforcing dependencies to flow inward and isolating the core domain from external dependencies.

# Credits and Acknowledgements
This project was developed as a freelance project and is the result of collaborative efforts. 
For any questions or inquiries, please contact with me or refer to the GitHub repository for additional information.

## [Wsuffy Git](https://github.com/Wsuffy)
