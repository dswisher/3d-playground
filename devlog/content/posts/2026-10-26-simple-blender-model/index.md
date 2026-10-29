+++
date = '2026-10-26T07:54:33-05:00'
draft = false
title = 'Simple Blender Model'
slug = 'simple-blender-model'
tags = []
+++

Going through the process of creating a simple model in Blender, exporting it, then importing it into raylib and rendering it.

# Getting Started with Blender

I've tinkered with Blender in the past, and even went through the "donut tutorial", but that was years ago, and I've forgotten it all.
There are a number of tutorials available on youtube:

- [Blender 5 Complete Beginners Guide](https://www.youtube.com/playlist?list=PLn3ukorJv4vv9_e-htADGsPX9TMaQpHV8) by [Grant Abbitt](https://www.youtube.com/@grabbitt) - this is the one I'm going through
   - [Part 1 - The Interface](https://www.youtube.com/watch?v=kVcY7K-JA1Y)
   - [Part 2 - Materials](https://www.youtube.com/watch?v=J2OpaKlChI8)
   - [Part 3 - The Old Man](https://youtube.com/watch?v=ut9_RRf1-hM)
   - [Part 4 - Old Man Details](https://www.youtube.com/watch?v=GB6xDutIOx0)
   - [Part 5 - Monsters Body](https://www.youtube.com/watch?v=YaWGsX1IyTk)
   - [Part 6 - Monsters Arms](https://www.youtube.com/watch?v=jZvSIupp53E)
   - [Part 7 - The Street](https://www.youtube.com/watch?v=ctBx6OQ4Ljg)
   - [Part 8 - Lights & Compositing](https://www.youtube.com/watch?v=7moxMNaRlk8)
- [Blender for Complete Beginners](https://www.youtube.com/playlist?list=PLsGl9GczcgBumq6fLmmR1ZYYn4EDkIg5B) by Ryan King
- [Blender Donut Tutorial (2025)](https://www.youtube.com/playlist?list=PLjEaoINr3zgGUwGwXlj9kBe7TrVWNjkyv) - by Blender Guru


There are also tutorials that walk through the process of building specific objects:

- [New to Blender? Start by Building a House!](https://www.youtube.com/watch?v=Bik3Xz88yW4) by CG Geek

I roughly followed this to get a house set up.
As of this writing, I'm only partway through, but I will continue improving the model later.
Blender has a lot of power, and is going to take time to learn.


# Exporting

I just did "Export" and selected "glb".
The seems to import just fine.

The only issue I see is that the roof looks totally uniform - the roof is all the same color.
I suspect this is because it is using a "flat" rendering, as there are no lights.
I need to dig into lighting a bit, to get it to look better (I think).

Even without the lighting, the house is recognizable.


# Rendering

The rendering of the model was straightforward.
Since I'm using my scene infrastructure, in the scene's `OnEnter` method, I loaded the model and checked it for validity:

```c#
houseModel = Raylib.LoadModel(path);

if (houseModel != null && !Raylib.IsModelValid(houseModel.Value))
{
    throw new InvalidOperationException($"Model in {path} is not valid.");
}
```

In the scene's `OnExit`, I unload the model:

```c#
if (houseModel != null)
{
    Raylib.UnloadModel(houseModel.Value);

    houseModel = null;
}
```


# Lighting

TBD

