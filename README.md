# unity-developer-test-snake

Existing code improvement test.

The goal of this test is to improve and modernize the existing code base to make it suitable for a live game.

You are asked to focus on those areas (prioritized):
1. Memory Management.
2. Making code easier to test, and adding tests.
3. In the future, we plan to start adding LiveOps to the game, so we will need to be able to update the game assets and config without requiring a new release.
4. Improve the maintainability and extendability of the code.
5. Reduce compile times.
6. In the future, we are considering releasing it on different platforms that might have different control schemes.

As there might be too much to be improved in this test, it should be taken as a time box. So:

- List the improvements that you can see as applicable to the test.
- Pick and implement the most important ones that you think you can do in the given time.
- Explain your choices on which to pick.


Please send back a zip file containing the whole project and the git history. We are not that much interested in the actual commits, but it makes it easier for us to review the test 🙂

This project is originally from Unity Code Monkey and has plenty of mentions of that all over. The original video can be seen here:
https://unitycodemonkey.com/video.php?v=ctCa1rH3CIc

----------------------------------------------------------------

## 0. Before Begins
First, thank you for this opportunity. It has been fun to modify this game. It takes me between 6-7 hours.
Test Made with Unity Version 2021.3.1f1 ( Same as the original project ).

### Analysis
First, I played the game and observed what happens in the inspector under certain actions:
General:
	-Each sound creates a GameObject
Main Menu:
	-All the UI is already instantiated under the same canvas
GamePlay
	-All gameObject in the root file, there is no order at all

Then, checking the code I see:
Negative:
  -No Subfolders
  -No namespaces.
  -Multiples classes in a script.
  -A lot of magic numbers.
  -Scripts with a lot of responsibilities.
  -A lot of static functions
  -Mix of logic and views
  -Lack of config files a.k.a. scriptable objects
  -Use of transform find.
  -Usage of Text instead of TextMesh Pro
  -No compression for the textures.
  -Excessive use of Singletons.
  -No Sprite atlas.

Positive:
  -There are a few managers.
  -The code is split.
  -Mostly, the methods are small.
  -Different scenes help to split the game functionality.

### First Improvements
I will add a service locator in order to make a better initial architecture
ServiceLocator: I didn't do this from scratch, I use the one located on this website but I add them an interface for IService and make the service locator static. 
To enable the services, for now, I create an Installer function and added it to the scriptable order. In a normal game, it will happen in an initial scene. I took some code from [here](https://thepowerups-learning.com/patrones-de-diseno-service-locator/)

## 1. Memory Management.
To improve the memory management, I will:
		-Avoid creating one object per sound improving the sound manager to use components instead of the GameObjects for the sound source
		-Avoid creating Apple in the game using the pool.
		-Avoid creating snake bodies using the pool.

## 2. Making code easier to test, and adding tests.
A lot of parts in the game are already in the ServiceLocator, which allows us to test the services easily.
To test more, we will add a few interfaces in the Snake class.
To be able to test, I add it to the library, [NSubstitue](https://nsubstitute.github.io/).
I only add one test for the score service. The same as this, we could add services for all the services.
In this personal project, you can find a lot of tests that I make for different services [Link](https://github.com/NornaGamesTeam/Urd/tree/develop/UdrProject/Assets/UrdPackage/Tests)

## 3. In the future, we plan to start adding LiveOps to the game, so we will need to be able to update the game assets and config without requiring a new release.
To do this, I added addressable into the game. I choose to make groups according to functionality. The groups should be set it depending on the project. Then I create a Service ( AssetService ), to handle the loading of the elements from the addressable.

## 4. Improve the maintainability and extendability of the code.
To do this, I move the logic to a service locator.
	Add a few interfaces to improve the quality of the code.
	Add a few scriptable objects to make the configuration of the game easy
		Initial Pool
		Sound Config
		GamePlay Config
	Move to constant the magic and the hard-coded strings.
	Split the multiple class in one file into a few files.

## 5. Reduce compile times.
To reduce the compilation time, I add it a few assemblies. I have some difficulties because the code is quite spaghetti. I would like to add something like [signals](https://github.com/yankooliveira/signals) or a similar message system, to make better communication in the game. For example, to communicate with the UI. By doing that, I would be able to create an assembly for the UI.
	
## 6. In the future, we are considering releasing it on different platforms that might have different control schemes.
Using the same mentality for the configs, we are adding configs into the addressable. This will allow us to generate different configs depending on the platform. So I create a component called ColorScheme, where using an enum, we can select what we want to change.
	
### 7. Conclusion
I did almost all the changes that I describe above as negative. 
The remaining part was improving the UI removing the singletons and the static functions. But In this test, I choose to focus on the logic and the architecture of that.
Also still some big methods in the code.
I could add a Signal or message system to avoid dependencies between different parts in the game, or at least, I could develop more events to avoid the direct call to other classes.
Hope you find the test interesting and if you have any questions, don't hesitate to ask.
