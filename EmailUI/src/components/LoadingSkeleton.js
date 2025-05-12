import React from 'react';
import styled, { keyframes } from 'styled-components';

const pulse = keyframes`
  0% {
    opacity: 0.6;
  }
  50% {
    opacity: 0.9;
  }
  100% {
    opacity: 0.6;
  }
`;

const SkeletonContainer = styled.div`
  margin-top: var(--spacing-xl, 2rem);
  padding: var(--spacing-lg, 1.5rem);
  background-color: var(--color-surface);
  border-radius: var(--border-radius-medium, 8px);
  border-left: 4px solid var(--color-border);
  transition: background-color 0.3s ease, border-color 0.3s ease;
`;

const SkeletonTitle = styled.div`
  height: 1.5rem;
  width: 150px;
  background-color: var(--color-skeleton);
  border-radius: var(--border-radius-small, 4px);
  margin-bottom: 1.25rem;
  animation: ${pulse} 1.5s infinite ease-in-out;
  transition: background-color 0.3s ease;
`;

const SkeletonLine = styled.div`
  height: 0.875rem;
  width: ${props => props.width || '100%'};
  background-color: var(--color-skeleton);
  border-radius: var(--border-radius-small, 4px);
  margin-bottom: 0.75rem;
  animation: ${pulse} 1.5s infinite ease-in-out;
  animation-delay: ${props => props.delay || '0s'};
  transition: background-color 0.3s ease;
`;

const LoadingSkeleton = () => {
  return (
    <SkeletonContainer>
      <SkeletonTitle />
      <SkeletonLine width="90%" />
      <SkeletonLine width="95%" delay="0.1s" />
      <SkeletonLine width="85%" delay="0.2s" />
      <SkeletonLine width="92%" delay="0.3s" />
      <SkeletonLine width="80%" delay="0.4s" />
      <SkeletonLine width="88%" delay="0.5s" />
    </SkeletonContainer>
  );
};

export default LoadingSkeleton;
