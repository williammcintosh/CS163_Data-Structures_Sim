# CS163_Data-Structures_Sim

As a computer science tutor, I found that teaching data structures is more successfully through a visualizer. At Portland State University, we choose to teach data structures intentionally separate from object oriented programming including inheritance. This makes the process of teaching class object instantiating a little tricky. We get around needing inheritance by creating a single object in int main() for the purpose of temporarily holding the char * information that will then be copied into each object in the data structure.

# How to Use the Simulator

### Write a new Title and Notes

Write them in the writable sections, then click outside of the boxes to make the submit button appear.

### Copy from Main to Temporary Object

![From int main](https://github.com/williammcintosh/CS163_Data-Structures_Sim/blob/main/images/DS_main_to_temp_obj.png)

What you wrote gets copied from int main() into the temporary object.

### Copy from Temporary Object to Data Structure Object

![From temp obj](https://github.com/williammcintosh/CS163_Data-Structures_Sim/blob/main/images/DS_temp_obj_to_obj.png)

A new node with a new class object is created, then the char stars that were copied into the temporary object are then copied into the newly created class object.
