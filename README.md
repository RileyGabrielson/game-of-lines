# Game of Lines

Inspired by [Conway's Game of Life](https://playgameoflife.com/).

## Behavior Rules

1. Active lines move 1 unit per iteration
2. If 2 active lines facing the same direction impact, they spawn a new line orthogonal to their impact.
3. If an active line impacts a stationary line, the active line's direction is reversed
4. If 2 active lines impact on an orthogonal line, the line is destroyed
