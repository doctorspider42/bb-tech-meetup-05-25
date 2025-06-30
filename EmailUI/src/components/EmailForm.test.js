import { fireEvent } from '@testing-library/react';
import axios from 'axios';
import EmailForm from './EmailForm';
import { renderWithProviders } from '../../testUtils';

var mockPost;
jest.mock('axios', () => {
  mockPost = jest.fn();
  return {
    create: () => ({ post: mockPost })
  };
});

const setup = () => renderWithProviders(<EmailForm />);

test('submit button disabled without input', () => {
  const { getByRole, getByLabelText } = setup();
  const button = getByRole('button', { name: /analyze email/i });
  expect(button).toBeDisabled();
  fireEvent.change(getByLabelText(/email content/i), {
    target: { value: 'Hello' },
  });
  expect(button).not.toBeDisabled();
});

test('shows error when API call fails', async () => {
  mockPost.mockRejectedValueOnce({ message: 'Network error' });
  const { getByRole, getByLabelText, findByText } = setup();
  fireEvent.change(getByLabelText(/email content/i), { target: { value: 'Hi' } });
  fireEvent.click(getByRole('button', { name: /analyze email/i }));
  expect(await findByText(/network error/i)).toBeInTheDocument();
});
