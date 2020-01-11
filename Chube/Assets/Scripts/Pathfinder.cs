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
		get => gCost+hCost;
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
		
		open = new List<Node>() { new Node(origin) };
		closed = new List<Node>() {};

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
		while (open.Count > 0)
		{
            Node current = open[0];
            foreach (Node node in open)
                if (node.fCost < current.fCost)
                    current = node;
            closed.Add(current);

            List<Node> children = new List<Node>()
            {
                new Node(current.position + new Vector3Int(+1, 0, 0)),
                new Node(current.position + new Vector3Int(-1, 0, 0)),
                new Node(current.position + new Vector3Int(0, +1, 0)),
                new Node(current.position + new Vector3Int(0, -1, 0)),
                new Node(current.position + new Vector3Int(0, 0, +1)),
                new Node(current.position + new Vector3Int(0, 0, -1)),
            };

            foreach (Node child in children)
            {
                if (tilemap.GetTile(child.position) == null)
                {
                    children.Remove(child);
                    continue;
                }

                child.parent = current;

                child.gCost = current.gCost +
                    Mathf.Abs(current.position.x - child.position.x) +
                    Mathf.Abs(current.position.y - child.position.y) +
                    Mathf.Abs(current.position.z - child.position.z);
                child.hCost =
                    Mathf.Abs(origin.x - child.position.x) +
                    Mathf.Abs(origin.y - child.position.y) +
                    Mathf.Abs(origin.z - child.position.z);

                foreach (Node node in open)
                    if (node.position == child.position)
                        if (node.fCost < child.fCost)
                        {
                            children.Remove(child);
                            continue;
                        }
                foreach (Node node in open)
                    if (node.position == child.position)
                        if (node.fCost < child.fCost)
                        {
                            children.Remove(child);
                            continue;
                        }

                if (child.position == destination)
                    return child;
            }
		}
        return null;
	}
}
