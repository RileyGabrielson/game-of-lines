using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacingLine : MonoBehaviour
{
    public LinesBoard board;
    public SpriteRenderer directionIndicator;

    private List<Line.Direction> directions = new List<Line.Direction>();
    private int curDirection = 0;
    private bool moving = true;

    void Start()
    {
      this.directions.Add(Line.Direction.UP);
      this.directions.Add(Line.Direction.RIGHT);
      this.directions.Add(Line.Direction.DOWN);
      this.directions.Add(Line.Direction.LEFT);
    }

    void Update()
    {
      SnapToMouse();
      if(Input.GetKeyDown(KeyCode.R)) ToggleDirection();
      if(Input.GetKeyDown(KeyCode.M)) ToggleMoving();
    }

    private void SnapToMouse()
    {
      transform.position = GetCurrentPosition();
    }

    private Vector3Int GetCurrentPosition()
    {
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      int x = Mathf.RoundToInt(worldPosition.x);
      int y = Mathf.RoundToInt(worldPosition.y);
      return new Vector3Int(x, y, 0);
    }

    public void AddLine()
    {
      Vector3Int worldPosition = GetCurrentPosition();
      Position pos = new Position(worldPosition.x, worldPosition.y);
      if(moving) 
      {
        board.AddLine(new MovingLine(pos, this.directions[curDirection]));
      }
      else
      {
        board.AddLine(new StationaryLine(pos, this.directions[curDirection]));
      }
    }

    public void ToggleMoving()
    {
      this.moving = !this.moving;
      this.directionIndicator.enabled = this.moving;
    }

    public void ToggleDirection()
    {
      int newIndex = curDirection += 1;
      if(newIndex >= this.directions.Count) newIndex = 0;
      this.curDirection = newIndex;

      Quaternion fromAngle = transform.rotation;
      Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, -90f));
      this.transform.rotation = toAngle;
    }
}
