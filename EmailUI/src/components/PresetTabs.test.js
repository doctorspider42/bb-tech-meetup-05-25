import { render, fireEvent } from '@testing-library/react';
import PresetTabs from './PresetTabs';
import { EmailProvider, useEmail } from '../contexts/EmailContext';
import { ThemeProvider } from '../contexts/ThemeContext';
import axios from 'axios';

jest.mock('axios', () => ({
  create: () => ({ post: jest.fn() })
}));

const Wrapper = () => {
  const { emailContent } = useEmail();
  return (
    <>
      <PresetTabs />
      <div data-testid="content">{emailContent}</div>
    </>
  );
};

const setup = () =>
  render(
    <ThemeProvider>
      <EmailProvider>
        <Wrapper />
      </EmailProvider>
    </ThemeProvider>
  );

test('shows preset description on click', () => {
  const { getByText } = setup();
  fireEvent.click(getByText('Spam'));
  expect(getByText(/unsolicited or irrelevant/)).toBeInTheDocument();
});

test('updates email content when preset clicked', () => {
  const { getByText, getByTestId } = setup();
  fireEvent.click(getByText('Product Question'));
  expect(getByTestId('content').textContent).toMatch(/SUPER2000/);
});
