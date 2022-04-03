# How to contribute

Thanks a lot for contributing to the development of AixOCAT. You help to make the Automation Technology world a bit better!

To give you some guidelines and to ensure the quality of AixOCAT's code, we collected our requirements in our wiki:

| Page  | About  |
|---|---|
| [Contribute to AixOCAT](https://github.com/RWTH-EBC/AixOCAT/wiki/Contribute-to-AixOCAT) | Make your own contribution to AixOCAT |
| [Git Workflow](https://github.com/RWTH-EBC/AixOCAT/wiki/Git-Workflow)  | How to manage working with Git and AixOCAT |
| [How to..](https://github.com/RWTH-EBC/AixOCAT/wiki/How-to..) | Divers helpful tutorials |

If you feel that something is missing, confuses you or is not in the right place, don't hesitate to put that in a new issue! If you'd like to contribute directly to AixOCAT, you can follow the steps below.

> Note: `[...]` indicates fields that you have to fill yourself, after removing the square brackets.

1. Create an issue to discuss your ideas with AixOCAT's maintainers

1. Clone the AixOCAT repository locally:
	- `git clone [SSH-Key/Https]`
	- `cd AixOCAT`
	
1. Create a new branch to add your feature:
	- `git branch [your branch name]`
	    - `[your branch name]` should follow this format: `i[issue  number]_[issue keywords]`, e.g. `i100_calibrate_sensors`
	
1. Navigate to your new branch: 
	- `git checkout [your branch name]`

1. After doing your changes, commit them:
	- `git add . && git -m "[Description of your changes] #[your issue number]"`
      - Example of a commit message: `"Add FB to calibrate sensors #100"` 
      - Reference the issue number so it's linked in your issue page.
	
1. Push your local changes to the remote repository:
	-  `git push -u origin [your branch name]`
	
1. Once you have finalized your contribution, you are ready to merge your branch. Go to the web interface of GitHub, navigate to `Pull requests`, then create a `New pull request`:
![image](https://user-images.githubusercontent.com/68941589/110147978-8d30e100-7ddc-11eb-80fd-6a590a548e8a.png)

1. Pick out `<your branch name>` from the drop down menu (3) then click on `Create pull request`
![image](https://user-images.githubusercontent.com/68941589/110148153-c8331480-7ddc-11eb-9e4e-48c03549c51b.png)

1. Finally, in the new `Open a pull request` view 
    1. Enter a comment such as `Closes #[your issue number]`
    2. Submit by clicking on `Create pull request`

The AixOCAT maintainers would take it from there. Thank you for your contributions. :slightly_smiling_face:
