import { render } from '@testing-library/react';
import LoadingSkeleton from './LoadingSkeleton';

// Simple render helper
const setup = () => render(<LoadingSkeleton />);

test('renders title and multiple skeleton lines', () => {
  const { container } = setup();
  const skeletonContainer = container.firstChild;
  // first child is container with 7 children (1 title + 6 lines)
  expect(skeletonContainer.children.length).toBe(7);
});
