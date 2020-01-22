using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node
{
    public Vector3Int position;

    public Node parent;
    public List<Node> children;

    public double gCost;
    public double hCost;
    public double fCost
    {
        get => gCost + hCost;
    }

    public Node(Vector3Int _position)
    {
        position = _position;
    }
}

public class Pathfinder
{
    Tilemap tilemap;

    Vector3Int origin;
    Vector3Int destination;

    List<Node> open;
    List<Node> closed;

    public List<Vector3Int> path;

    public Pathfinder(Tilemap _tilemap, Vector3Int _origin, Vector3Int _destination)
    {
        tilemap = _tilemap;

        origin = _origin;
        destination = _destination;
<<<<<<< HEAD
=======
        Debug.Log(origin + " : " + destination);
>>>>>>> troops

        open = new List<Node>() { new Node(origin) };
        closed = new List<Node>() { };

        path = CalculatePath(Pathfind());
    }

    private List<Vector3Int> CalculatePath(Node node)
    {
        List<Vector3Int> path = new List<Vector3Int>();

        Node current = node;
        while (true)
        {
            path.Add(current.position);

            if (current.parent == null)
                break;
            else
                current = current.parent;
        }

        return path;
    }

    private Node Pathfind()
    {
        int turnCount = 0;
        while (open.Count > 0 && turnCount < 100)
        {
            Node current = open[0];
            foreach (Node node in open)
                if (node.fCost < current.fCost)
                    current = node;
            open.Remove(current);
<<<<<<< HEAD

            Debug.Log(current.position);
=======
>>>>>>> troops

            List<Node> children = new List<Node>()
            {
                new Node(current.position + new Vector3Int(+1, 0, 0)),
                new Node(current.position + new Vector3Int(-1, 0, 0)),
                new Node(current.position + new Vector3Int(0, +1, 0)),
                new Node(current.position + new Vector3Int(0, -1, 0)),
            };

            List<Node> remove = new List<Node>();
            foreach (Node child in children)
            {
                TileBase tile = tilemap.GetTile(child.position);
                if (tile == null || tile.name != "BuildingTile")
                {
                    remove.Add(child);
                    continue;
                }
                child.parent = current;

                if (child.position == destination)
                    return child;

                child.gCost = current.gCost + 1;
                child.hCost =
                    Mathf.Abs(origin.x - child.position.x) +
                    Mathf.Abs(origin.y - child.position.y) +
                    Mathf.Abs(origin.z - child.position.z);

                foreach (Node node in open)
                    if (node.position == child.position)
                        if (node.fCost < child.fCost)
                        {
                            remove.Add(child);
                            continue;
                        }
                foreach (Node node in open)
                    if (node.position == child.position)
                        if (node.fCost < child.fCost)
                        {
                            remove.Add(child);
                            continue;
                        }

                if (child.position == destination)
                    return child;
            }
            foreach (Node node in remove)
                children.Remove(node);

            turnCount++;
            closed.Add(current);
            open.AddRange(children);
        }
        return null;
    }
}
